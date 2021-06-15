using System;
using System.Collections.Generic;
using System.Text;

namespace ArvoreRedBlack
{
    public class Arvore
    {
        protected No raiz;

        public void Inserir(int key, int dados)
        {
            if (this.raiz == null)
            {
                this.raiz = new No(key, dados, null);
                this.raiz.cor = "black";
            }
            else
            {
                No novoNo = this.raiz.Persistir(key, dados);
                this.isVermelhoConsecutivo(novoNo);
                if (this.raiz.cor == "red")
                {
                    this.raiz.cor = "black";
                }
            }
        }

        public int Delete(int key)
        {
            if (this.raiz.key == key)
            {
                if (this.raiz.filhoEsquerdo == null && this.raiz.filhoDireito == null)
                {
                    var objeto = this.raiz.dados;
                    this.raiz = null;
                    return objeto;
                }
                else if (this.raiz.filhoEsquerdo != null && this.raiz.filhoDireito == null)
                {
                    var objeto = this.raiz.dados;
                    this.raiz = this.raiz.filhoEsquerdo;
                    this.raiz.noPai = null;
                    return objeto;
                }
                else if (this.raiz.filhoEsquerdo == null && this.raiz.filhoDireito != null)
                {
                    var objeto = this.raiz.dados;
                    this.raiz = this.raiz.filhoDireito;
                    this.raiz.noPai = null;
                    return objeto;
                }
                else
                {
                    var sucessor = this.raiz.Sucessor();
                    var objeto = raiz.dados;
                    this.Delete(sucessor.key);
                    this.raiz.key = sucessor.key;
                    this.raiz.dados = sucessor.dados;
                    return objeto;
                }
            }
            else
            {
                No node = this.raiz.Consultar(key);
                var objeto = node.dados;
                if (node.filhoEsquerdo == null && node.filhoDireito == null)
                {
                    if (node.noPai.filhoEsquerdo.key == node.key)
                    {
                        node.noPai.filhoEsquerdo = null;
                    }
                    else
                    {
                        node.noPai.filhoDireito = null;
                    }
                    return objeto;
                }
                else if (node.filhoEsquerdo != null && node.filhoDireito == null)
                {
                    if (node.noPai.filhoEsquerdo.key == node.key)
                    {
                        node.noPai.filhoEsquerdo = node.filhoEsquerdo;
                    }
                    else
                    {
                        node.noPai.filhoDireito = node.filhoEsquerdo;
                    }
                    return objeto;
                }
                else if (node.filhoEsquerdo == null && node.filhoDireito != null)
                {
                    if (node.noPai.filhoDireito.key == node.key)
                    {
                        node.noPai.filhoDireito = node.filhoDireito;
                    }
                    else
                    {
                        node.noPai.filhoEsquerdo = node.filhoDireito;
                    }
                    return objeto;
                }
                else if (node.filhoEsquerdo != null && node.filhoDireito != null)
                {
                    var sucessor = node.Sucessor();
                    objeto = node.dados;
                    this.Delete(sucessor.key);
                    node.key = sucessor.key;
                    node.dados = sucessor.dados;
                    return objeto;
                }
                else
                {
                    if (node.noPai.filhoDireito.key == node.key)
                    {
                        node.noPai.filhoDireito = null;
                    }
                    else
                    {
                        node.noPai.filhoEsquerdo = null;
                    }
                    return objeto;
                }
            }
        }

        public No Consultar(int key)
        {
            return this.raiz.Consultar(key);
        }

        public void VerificarQuaseBalanceamento(No atual)
        {
            var avo = atual.noPai != null ? atual.noPai.noPai : null;

            if (avo != null)
            {
                if (avo.filhoDireito.key == atual.noPai.key)
                {
                    if (atual.noPai.filhoDireito != null && atual.noPai.filhoDireito.key == atual.key)
                    {
                        this.RotacaoSimplesEsquerda(atual);
                        Console.WriteLine("A árvore ta desbalanceada e receberá rotação simples para a esquerda");
                        this.InvertColor(atual.noPai);
                        this.InvertColor(avo);
                    }
                    else
                    {
                        this.DuplaRotacaoDireitaEsquerda(atual);
                        Console.WriteLine("A árvore ta desbalanceada e receberá rotação dupla para a esquerda");
                        this.InvertColor(atual);
                        this.InvertColor(avo);
                    }
                }
                else
                {
                    if (atual.noPai.filhoEsquerdo != null && atual.noPai.filhoEsquerdo.key == atual.key)
                    {
                        this.RotacaoSimplesDireita(atual);
                        Console.WriteLine("A árvore ta desbalanceada e receberá rotação simples para a direita");
                        this.InvertColor(atual.noPai);
                        this.InvertColor(avo);
                    }
                    else
                    {
                        this.DuplaRotacaoEsquerdaDireita(atual);
                        Console.WriteLine("A árvore ta desbalanceada e receberá rotação dupla para a direita");
                        this.InvertColor(atual);
                        this.InvertColor(avo);
                    }
                }
            }
            if (atual.noPai != null)
            {
                this.isVermelhoConsecutivo(atual.noPai.noPai);
            }
            else
            {
                this.raiz = atual;
            }
        }
        public No RotacaoSimplesDireita(No inicial)
        {
            No pai = inicial.noPai;
            No avo = pai.noPai;

            No t1 = inicial.filhoEsquerdo,
                t3 = pai.filhoDireito;

            avo.filhoEsquerdo = t3;

            if (t3 != null)
            {
                t3.noPai = avo;
            }

            pai.noPai = avo.noPai;

            if (pai.noPai != null)
            {
                if (pai.noPai.key > pai.key)
                {
                    pai.noPai.filhoEsquerdo = pai;
                }
                else
                {
                    pai.noPai.filhoDireito = pai;
                }
            }

            avo.noPai = pai;
            pai.filhoDireito = avo;

            if (this.raiz.key == avo.key)
            {
                this.raiz = pai;
            }

            return inicial;
        }

        public No RotacaoSimplesEsquerda(No inicial)
        {
            No pai = inicial.noPai;
            No avo = pai.noPai;

            No t5 = inicial.filhoDireito,
                    t3 = pai.filhoEsquerdo,
                    t4 = inicial.filhoEsquerdo;

            pai.noPai = avo.noPai;

            if (pai.noPai != null)
            {
                if (pai.noPai.key > pai.key)
                {
                    pai.noPai.filhoEsquerdo = pai;
                }
                else
                {
                    pai.noPai.filhoDireito = pai;
                }
            }
            pai.filhoEsquerdo = avo;
            avo.noPai = pai;

            avo.filhoDireito = t3;

            if (t3 != null)
            {
                t3.noPai = avo;
            }

            if (this.raiz.key == avo.key)
            {
                this.raiz = pai;
            }
            return inicial;
        }

        public No DuplaRotacaoEsquerdaDireita(No inicial)
        {
            var rotacao = RotacaoSimplesEsquerda(inicial);
            RotacaoSimplesDireita(rotacao);

            return rotacao;
        }

        public No DuplaRotacaoDireitaEsquerda(No inicial)
        {
            var rotacao = RotacaoSimplesDireita(inicial);
            RotacaoSimplesEsquerda(rotacao);

            return rotacao;
        }

        public void print(int max_altura, int n)
        {
            n += 1;
            Dictionary<int, List<No>> listaNiveis = new Dictionary<int, List<No>>();
            //int max_altura = 3;

            for (int i = max_altura; i >= 0; i--)
            {
                listaNiveis[i] = new List<No>();
            }

            listaNiveis[max_altura].Add(this.raiz);
            for (int i = max_altura - 1; i >= 0; i--)
            {
                foreach (No noAtual in listaNiveis[i + 1])
                {
                    if (noAtual == null)
                    {
                        listaNiveis[i].Add(null);
                        listaNiveis[i].Add(null);
                    }
                    else
                    {
                        listaNiveis[i].Add(noAtual.filhoEsquerdo);
                        listaNiveis[i].Add(noAtual.filhoDireito);
                    }
                }
            }

            StringBuilder sb;
            for (int i = max_altura; i >= 0; i--)
            {
                int size = (int)(Math.Pow(2, i + 1) * n);
                sb = new StringBuilder("|");
                foreach (No no in listaNiveis[i])
                {
                    if (no == null)
                    {
                        sb.Append(this.centerString(" ", size));
                    }
                    else
                    {
                        sb.Append(this.centerString(no.dados.ToString() + no.cor[0], size));
                    }
                }
                sb.Append("|");
                Console.WriteLine(sb.ToString());
            }
        }

        private string centerString(string value, int size)
        {
            string center = value;
            decimal aux = (size - value.Length) / 2;
            //Console.WriteLine("aux: {0}", aux);
            int fill_size = (int)Math.Floor(aux);
            StringBuilder sb = new StringBuilder(size);

            for (int i = 0; i < fill_size; i++)
            {
                sb.Append(" ");
            }

            if ((size - value.Length) % 2 == 1)
            {
                sb.Append(" ");
            }

            sb.Append(center);

            for (int i = 0; i < fill_size; i++)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public void InvertColor(No node)
        {
            if (node != null)
            {
                if (node.cor == "black")
                {
                    node.cor = "red";
                }
                else
                {
                    node.cor = "black";
                }
            }
        }

        public void UncleRed(No node)
        {
            var avo = node.noPai.noPai;

            this.InvertColor(avo);
            this.InvertColor(avo.filhoDireito);
            this.InvertColor(avo.filhoEsquerdo);
        }
        public void UncleBlack(No node)
        {
            this.VerificarQuaseBalanceamento(node);
        }
        public void isVermelhoConsecutivo(No node)
        {
            if (node == null) return;
            var avo = node.noPai != null ? node.noPai.noPai : null;

            if (node.noPai != null && node.noPai.cor == "red")
            {
                if (avo != null)
                {
                    if (avo.filhoEsquerdo != null && avo.filhoDireito != null)
                    {
                        if (avo.filhoDireito.key == node.noPai.key)
                        {
                            if (avo.filhoEsquerdo.cor == "red")
                            {
                                this.UncleRed(node);
                            }
                            else
                            {
                                UncleBlack(node);
                            }
                        }
                        else if (avo.filhoDireito.cor == "red")
                        {
                            this.UncleRed(node);
                        }
                        else
                        {
                            this.UncleBlack(node);
                        }
                    }
                    else
                    {
                        this.UncleBlack(node);
                    }
                    this.isVermelhoConsecutivo(avo);
                }
            }
        }
    }
}
