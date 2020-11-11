namespace VivaLaDama.Models
{
    public class CampoDiGioco
    {
        private const int DEFAULT_LENGTH = 8;
        public Pedina[,] Campo { get; }
        public CampoDiGioco()
        {
            int numPedineBianche, numPedineNere;
            numPedineBianche = numPedineNere = 0;

            Campo = new Pedina[DEFAULT_LENGTH, DEFAULT_LENGTH];

            for(var i=0; i<DEFAULT_LENGTH ;i++)
            {
                for(var j=0; j<DEFAULT_LENGTH ;j++)
                {
                    Campo[i, j] = null;//Casella vuota (nessuna pedina sopra)

                    if((i+j)%2!=0)
                    {
                        if(j<=2)//Posiziono le pedine nere nelle prime tre righe
                        {
                            Campo[i, j] = new Pedina(Pedina.ColorePedina.BLACK, numPedineNere++);
                        }
                        else if(j>=DEFAULT_LENGTH-3)//Posizione le pedine bianche nelle ultime tre righe
                        {
                            Campo[i, j] = new Pedina(Pedina.ColorePedina.WHITE, numPedineBianche++);
                        }
                    }
                }
            }
        }
        private bool EvaluateMossa(Mossa mossa)
        {
            Coordinate start = GetPedina(mossa.Target);
            bool ret = false;

            if(start!=null && //Check se esiste la pedina
               mossa.From.IsValid(DEFAULT_LENGTH) && //Check sulle coordinate di partenza
               mossa.To.IsValid(DEFAULT_LENGTH) && //Check sulle coordinate di arrivo
               mossa.Target.Color!=Pedina.ColorePedina.NONE && //Check sul colore della pedina sia valido
               start.Equals(mossa.From) && //Check partenzaClient==partenzaServer
               IsEmpty(mossa.To)) //Check che l'arrivo sia una casella vuota
            {
                if (mossa.Target.Upgraded)//Check se è un damone
                {
                    ret = ret || CheckMossa(start.GetDownLeft(),
                                            start.GetDownLeft().GetDownLeft(),
                                            mossa.To,
                                            mossa.Target.GetColoreOpponente());
                    ret = ret || CheckMossa(start.GetDownRight(),
                                            start.GetDownRight().GetDownRight(),
                                            mossa.To,
                                            mossa.Target.GetColoreOpponente());
                    ret = ret || CheckMossa(start.GetUpLeft(),
                                            start.GetUpLeft().GetUpLeft(),
                                            mossa.To,
                                            mossa.Target.GetColoreOpponente());
                    ret = ret || CheckMossa(start.GetUpRight(),
                                            start.GetUpRight().GetUpRight(),
                                            mossa.To,
                                            mossa.Target.GetColoreOpponente());
                }
                else
                {
                    switch(mossa.Target.Color)
                    {
                        case Pedina.ColorePedina.BLACK:
                            ret = ret || CheckMossa(start.GetDownLeft(),
                                                    start.GetDownLeft().GetDownLeft(),
                                                    mossa.To,
                                                    mossa.Target.GetColoreOpponente());
                            ret = ret || CheckMossa(start.GetDownRight(),
                                                    start.GetDownRight().GetDownRight(),
                                                    mossa.To,
                                                    mossa.Target.GetColoreOpponente());
                            break;
                        case Pedina.ColorePedina.WHITE:
                            ret = ret || CheckMossa(start.GetUpLeft(),
                                                    start.GetUpLeft().GetUpLeft(),
                                                    mossa.To,
                                                    mossa.Target.GetColoreOpponente());
                            ret = ret || CheckMossa(start.GetUpRight(),
                                                    start.GetUpRight().GetUpRight(),
                                                    mossa.To,
                                                    mossa.Target.GetColoreOpponente());
                            break;
                    }
                }
            }

            return ret;
        }
        private Coordinate GetPedina(Pedina pedina)
        {
            for (var i=0; i<DEFAULT_LENGTH; i++)
            {
                for (var j=0; j<DEFAULT_LENGTH; j++)
                {
                    if(Campo[i, j].Equals(pedina))
                    {
                        return new Coordinate(i, j);
                    }
                }
            }

            return null;
        }
        private bool IsEmpty(Coordinate c)
        {
            if(c.IsValid(DEFAULT_LENGTH))
            {
                return Campo[c.Riga, c.Colonna] == null;
            }
            return false;
        }
        private bool IsNotEmpty(Coordinate c)
        {
            if(c.IsValid(DEFAULT_LENGTH))
            {
                return !IsEmpty(c);
            }
            return false;
        }
        private Pedina.ColorePedina GetColor(Coordinate c)
        {
            if(IsNotEmpty(c))
            {
                return Campo[c.Riga, c.Colonna].Color;
            }
            return Pedina.ColorePedina.NONE;
        }
        private bool CheckMossa(Coordinate dest1, Coordinate dest2, Coordinate finalDest, Pedina.ColorePedina coloreOpponente)
        {
            Pedina.ColorePedina coloreTrovato = GetColor(dest1);
            bool ret;

            ret = dest1.Equals(finalDest);//Provo a muovermi di uno
            ret = ret || (coloreTrovato==coloreOpponente &&
                          dest2.Equals(finalDest));//Provo a muovermi di due

            return ret;
        }
        public bool ExecuteMossa(Mossa mossa)
        {
            bool ret = EvaluateMossa(mossa);

            if(ret==true)
            {
                UpdateCampo(mossa.From,
                            mossa.From.GetDownLeft(),
                            mossa.From.GetDownLeft().GetDownLeft(),
                            mossa.To,
                            mossa.Target.GetColoreOpponente());
                UpdateCampo(mossa.From,
                            mossa.From.GetDownRight(),
                            mossa.From.GetDownRight().GetDownRight(),
                            mossa.To,
                            mossa.Target.GetColoreOpponente());
                UpdateCampo(mossa.From,
                            mossa.From.GetUpLeft(),
                            mossa.From.GetUpLeft().GetUpLeft(),
                            mossa.To,
                            mossa.Target.GetColoreOpponente());
                UpdateCampo(mossa.From,
                            mossa.From.GetUpRight(),
                            mossa.From.GetUpRight().GetUpRight(),
                            mossa.To,
                            mossa.Target.GetColoreOpponente());

                if(mossa.From.Riga==0 || mossa.From.Riga==DEFAULT_LENGTH-1)//Imposto il damone
                {
                    Campo[mossa.From.Riga, mossa.From.Colonna].Upgraded = true;
                }
            }

            return ret;
        }
        private bool UpdateCampo(Coordinate from, Coordinate dest1, Coordinate dest2, Coordinate finalDest, Pedina.ColorePedina coloreOpponente)
        {
            bool ret = CheckMossa(dest1, dest2, finalDest, coloreOpponente);

            if(ret)
            {
                if(IsEmpty(dest1))//Mi sposto e basta
                {
                    Campo[dest1.Riga, dest1.Colonna] = Campo[from.Riga, from.Colonna];
                }
                else//Ho mangiato qualcuno
                {
                    Campo[dest1.Riga, dest1.Colonna] = null;
                    Campo[dest2.Riga, dest2.Colonna] = Campo[from.Riga, from.Colonna];
                }

                Campo[from.Riga, from.Colonna] = null;
            }

            return ret;
        }
    }
}
