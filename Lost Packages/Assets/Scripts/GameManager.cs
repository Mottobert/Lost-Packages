﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int spielerIndex;
    public int spielerStartIndex;

    public int paketIndex;
    public int paketStartIndex;
    private int[] paketRandomStartSpots = new int[] {26, 27, 28, 31, 32, 33, 35, 36, 37};

    public int anzahlHolz;
    private int[] holzStartIndex;
    public GameObject[] holz;

    private int[] gegenstandSpots;

    public GameObject spieler;
    public GameObject paket;
    public GameObject holzplanke;

    public Text zuegeLabel;
    public int zuege;


    // Start is called before the first frame update
    void Start()
    {
        GegenstandSpotsInit();
        GameInit();
        ShowZuege();
    }


    //Instantiert den Spieler, das Paket und die Holzplanken auf dem Spielfeld 
    void GameInit()
    {
        holz = new GameObject[anzahlHolz];
        GameObject parent;

        holzStartIndex = new int[anzahlHolz];
        holzStartIndex = PickSpots(anzahlHolz);

        for (int i = 0; i < holz.Length; i++)
        {
            parent = GameObject.Find("Kachel " + holzStartIndex[i]);
            holz[i] = Instantiate(holzplanke, parent.transform.position, Quaternion.Euler(0, 0, 0));
            holz[i].GetComponent<GegenstandBewegung>().index = holzStartIndex[i];
        }

        parent = GameObject.Find("Kachel " + paketStartIndex);
        Instantiate(paket, parent.transform.position, Quaternion.Euler(0, 0, 0));
        GameObject.FindGameObjectWithTag("Paket").GetComponent<PaketBewegung>().index = paketStartIndex;

        parent = GameObject.Find("Kachel " + spielerStartIndex);
        Instantiate(spieler, parent.transform.position, parent.transform.rotation);
        GameObject.FindGameObjectWithTag("Spieler").GetComponent<SpielerBewegung>().index = paketStartIndex;
    }

    //Initiiert das globale Array, welches die freien Felder auf dem Spielfeld enthält
    void GegenstandSpotsInit()
    {
        gegenstandSpots = new int[38];

        for (int i = 0; i < gegenstandSpots.Length; i++)
        {
            gegenstandSpots[i] = i;
        }

        gegenstandSpots[spielerStartIndex] = 0;


        int rand = Random.Range(0, 8);
        paketStartIndex = paketRandomStartSpots[rand];
        gegenstandSpots[paketStartIndex] = 0;

        string[] nameO = GameObject.Find("Kachel " + paketStartIndex).GetComponent<Kachel>().GetFlow((int)GameObject.Find("Kachel " + paketStartIndex).GetComponent<Kachel>().transform.rotation.eulerAngles.z).GetComponent<Kachel>().name.Split(' ');

        int tempIndex = int.Parse(nameO[1]);
        gegenstandSpots[tempIndex] = 0;
    }


    //Gibt ein Array der Länge "anzahl" mit indices der freien Kacheln für Gegenstände zurück
    private int[] PickSpots(int anzahl)
    {
        int[] spots = new int[anzahl];
        int rand;

        int j = 0;
        while (j < anzahl)
        {
            rand = Random.Range(1, 37);

            if (gegenstandSpots[rand] != 0)
            {
                spots[j] = rand;
                gegenstandSpots[rand] = 0;

                string[] nameO = GameObject.Find("Kachel " + rand).GetComponent<Kachel>().GetFlow((int)GameObject.Find("Kachel " + rand).GetComponent<Kachel>().transform.rotation.eulerAngles.z).GetComponent<Kachel>().name.Split(' ');

                int tempIndex = int.Parse(nameO[1]);
                gegenstandSpots[tempIndex] = 0;

                j++;
            }
        }
        return spots;
    }


    //Zuganzeige
    public void SetZuege(int value)
    {
        zuege += value;
    }

    public int GetZuege()
    {
        return zuege;
    }

    public void ShowZuege()
    {
        zuegeLabel.GetComponent<Text>().text = "Verbleibende Züge " + zuege;
    }

}