using System;

namespace ArvoreRedBlack
{
    class Program
    {
        static void Main(string[] args)
        {
            //20, 15, 25, 12, 17, 24, 30, 10, 14, 13
            Arvore arv = new Arvore();

            arv.Inserir(20, 20);
            arv.Inserir(15, 15);
            arv.Inserir(25, 25);
            arv.Inserir(12, 12);
            arv.Inserir(17, 17);
            arv.Inserir(24, 24);
            arv.Inserir(30, 30);
            arv.Inserir(10, 10);
            arv.Inserir(14, 14);
            arv.Inserir(13, 13);
            arv.Inserir(40, 40);
            arv.Inserir(50, 50);
            arv.Inserir(51, 51);

            arv.print(5, 2);
        }
    }
}
