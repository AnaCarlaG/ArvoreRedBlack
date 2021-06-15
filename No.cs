using System;
using System.Collections.Generic;
using System.Text;

namespace ArvoreRedBlack
{
    public class No
    {
        public No(int key, int dados, No noPai)
        {
            this.key = key;
            this.dados = dados;
            this.noPai = noPai;
            this.cor = "red";
        }

        public int dados { get; set; }
        public int key { get; set; }
        public string cor { get; set; }
        public No filhoEsquerdo { get; set; }
        public No filhoDireito { get; set; }
        public No noPai { get; set; }

        public No Persistir(int key, int dados)
        {
            return this.AdicionarOrAtualizarRecursivo(key, dados);
        }
        private No AdicionarOrAtualizarRecursivo(int key, int dados)
        {
            if (this.key == key)
            {
                this.dados = dados;
                return this;
            }
            else if (this.key > key)
            {
                if (this.filhoEsquerdo != null)
                {
                    return filhoEsquerdo.AdicionarOrAtualizarRecursivo(key, dados);
                }
                else
                {
                    filhoEsquerdo = new No(key, dados, this);
                    return filhoEsquerdo;
                }
            }
            else
            {
                if (this.filhoDireito != null)
                {
                    return filhoDireito.AdicionarOrAtualizarRecursivo(key, dados);
                }
                else
                {
                    filhoDireito = new No(key, dados, this);
                    return filhoDireito;
                }
            }
        }

        public No Sucessor()
        {
            if (this.filhoDireito != null)
            {
                return this.filhoDireito.Minimo();
            }
            return this;
        }
        public No Antecessor()
        {
            if (this.filhoEsquerdo != null)
            {
                return this.filhoEsquerdo.Maximo();
            }
            return this;
        }
        public No Maximo()
        {
            if (this.filhoDireito != null)
            {
                return this.filhoDireito.Maximo();
            }
            return this;
        }
        public No Minimo()
        {
            if (this.filhoEsquerdo != null)
            {
                return filhoEsquerdo.Minimo();
            }
            return this;
        }

        public bool isFolha()
        {
            if (this.filhoEsquerdo == null && this.filhoDireito == null)
            {
                return true;
            }
            return false;
        }
        public No Consultar(int key)
        {
            return this.ConsultaRecursiva(key);
        }
        private No ConsultaRecursiva(int key)
        {
            if (this.key == key)
            {
                return this;
            }
            else if (this.key > key)
            {
                if (this.filhoEsquerdo != null)
                {
                    return this.filhoEsquerdo.ConsultaRecursiva(key);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (this.filhoDireito != null)
                {
                    return this.filhoDireito.ConsultaRecursiva(key);
                }
                else
                {
                    return null;
                }
            }
        }

        public int getProfundidade()
        {
            int alturaFilhoEsquerdo = 0, alturaFilhoDireito = 0;
            if (this.isFolha())
            {
                return 1;
            }
            if (this.filhoEsquerdo != null)
            {
                alturaFilhoEsquerdo = this.filhoEsquerdo.getProfundidade();
            }
            if (this.filhoDireito != null)
            {
                alturaFilhoDireito = this.filhoDireito.getProfundidade();
            }
            if (alturaFilhoDireito > alturaFilhoEsquerdo)
            {
                return alturaFilhoDireito + 1;
            }
            else
            {
                return alturaFilhoEsquerdo + 1;
            }
        }
        }
}
