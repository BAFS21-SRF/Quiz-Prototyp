using System;
using System.Collections.Generic;

[Serializable]
public class Frage
{
    public string frageText;
    public List<Auswahlmoeglichkeiten> auswahlmoeglichkeiten;
    public int id;
}

[Serializable]
public class Auswahlmoeglichkeiten
{
   public string auswahlText;
    public int order;
    public int frageId;
    public int id;
    public string assetId;

}