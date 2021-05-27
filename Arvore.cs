using System;
using System.Collections.Generic;
using System.Text;

namespace ArvoreAVL
{
    public class Arvore
    {
        protected No raiz;

        public void Inserir(int key, int dados)
        {
            if (this.raiz == null)
            {
                this.raiz = new No(key, dados, null);
            }
            else
            {
                No novoNo = this.raiz.Persistir(key, dados);
                this.VerificarBalanceamento(novoNo);
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

        public void VerificarBalanceamento(No atual)
        {
            setBalanceamento(atual);
            var balanceamento = atual.balanceamento;

            if(balanceamento == -2)
            {
                if(atual.getAltura(atual.filhoEsquerdo.filhoEsquerdo) >= atual.getAltura(atual.filhoEsquerdo.filhoDireito))
                {
                    Console.WriteLine("A árvore ta desbalanceada e receberá rotação simples para a direita");
                    atual = RotacaoSimplesDireita(atual);
                }
                else
                {
                    Console.WriteLine("A árvore ta desbalanceada e receberá rotação dupla para a esquerda");
                    atual = DuplaRotacaoEsquerdaDireita(atual);
                }
            }
            else if (balanceamento == 2)
            {
                if(atual.getAltura(atual.filhoDireito.filhoDireito) >= atual.getAltura(atual.filhoDireito.filhoEsquerdo))
                {
                    Console.WriteLine("A árvore ta desbalanceada e receberá rotação simples para a esquerda");
                    atual = RotacaoSimplesEsquerda(atual);
                }
                else 
                {
                    Console.WriteLine("A árvore ta desbalanceada e receberá rotação dupla para a direita");
                    atual = DuplaRotacaoDireitaEsquerda(atual);
                }
            }

            if(atual.noPai != null)
            {
                VerificarBalanceamento(atual.noPai);
            }
            else
            {
                this.raiz = atual;
            }
        }

        public No RotacaoSimplesDireita(No inicial)
        {
            this.print(2, 2);
            Console.WriteLine();
            No esquerda = inicial.filhoEsquerdo;
            esquerda.noPai = inicial.noPai;

            inicial.filhoEsquerdo = esquerda.filhoDireito;

            if(inicial.filhoEsquerdo != null)
            {
                inicial.filhoEsquerdo.noPai = inicial;
            }

            esquerda.filhoDireito = inicial;
            inicial.noPai = esquerda;

            if(esquerda.noPai != null)
            {
                if(esquerda.noPai.filhoDireito == inicial)
                {
                    esquerda.noPai.filhoDireito = esquerda;
                }
                else if(esquerda.noPai.filhoEsquerdo == inicial)
                {
                    esquerda.noPai.filhoEsquerdo = esquerda;
                }
            }

            setBalanceamento(inicial);
            setBalanceamento(esquerda);
           // Console.WriteLine("Rotacao simples para a direita");
            return esquerda;
        }

        public No RotacaoSimplesEsquerda(No inicial)
        {
            this.print(2, 2);
            Console.WriteLine();
            No direita = inicial.filhoDireito;
            direita.noPai = inicial.noPai;

            inicial.filhoDireito = direita.filhoEsquerdo;

            if(inicial.filhoDireito != null)
            {
                inicial.filhoDireito.noPai = inicial;
            }

            direita.filhoEsquerdo = inicial;
            inicial.noPai = direita;

            if(direita.noPai != null)
            {
                if(direita.noPai.filhoDireito == inicial)
                {
                    direita.noPai.filhoDireito = direita;
                }
                else if( direita.noPai.filhoEsquerdo == inicial)
                {
                    direita.noPai.filhoEsquerdo = direita;
                }
            }

            setBalanceamento(inicial);
            setBalanceamento(direita);
           // Console.WriteLine("Rotacao simples para a esquerda");
            return direita;
        }

        public No DuplaRotacaoEsquerdaDireita(No inicial)
        {
            inicial.filhoEsquerdo = RotacaoSimplesEsquerda(inicial.filhoEsquerdo);
           // Console.WriteLine("Rotacao Dula para a direita");
            return RotacaoSimplesDireita(inicial);
        }

        public No DuplaRotacaoDireitaEsquerda(No inicial)
        {
            inicial.filhoDireito = RotacaoSimplesDireita(inicial.filhoDireito);
            //Console.WriteLine("Rotacao Dula para a esquerda");
            return RotacaoSimplesEsquerda(inicial);
        }

        public void setBalanceamento(No no)
        {
            no.balanceamento = (no.getAltura(no.filhoDireito) - no.getAltura(no.filhoEsquerdo));
        }

        public void print(int max_altura, int n)
        {
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
                        sb.Append(this.centerString(no.dados.ToString(), size));
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
    }
}
