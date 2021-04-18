using System;
using System.Collections.Generic;

[Serializable]
public class Frage
{
    public string frageText;
    public List<Auswahl> auswahlmoeglichkeiten;
    public long id;
}

[Serializable]
public class Auswahl
{
    public long id;
    public int order;

    public long frageId;

    public string auswahlText;

}