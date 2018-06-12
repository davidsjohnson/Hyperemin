﻿using UnityEngine;
using System.Collections;
using System.Text;
using System;
using Sanford.Multimedia.Midi;

public class PlayerCtrl : MonoBehaviour
{        
    // Leaving for now. TODO: remove this at some point?
    public bool noteCtrlOn = true;

    public int minMidiNote = 36;
    public int maxMidiNote = 72;

    public GameObject completedMenuPrefab;

    public string ParticipantID { get; set; }           // Participant ID
    public string SessionNum { get; set; }              // Session Number - to help keep track of log files
    public string MidiScoreResource { get; set; }       // Name of file containing Midi data
    public string MidiInputDeviceName { get; set; }     // Name of Midi Device to connect to

    public LogWriter Logger { get; private set; }
    public MidiInputCtrl MidiIn { get; private set; }   // Main Midi Input Controller used to position left and right hands

    public static PlayerCtrl Control { get; private set; }     // Singleton Accessor

    private GameObject completedMessage;
    private GameObject completedMenu;

    void Awake ()
    {
        //Implement Psuedo-Singleton
        if (Control == null)
        {
            DontDestroyOnLoad(gameObject);
            Control = this;
        }
        else if (Control != this)
        {
            Destroy(gameObject);
        }

        //Initialize Midi In (so objects can subscribe to it upon load)
        MidiIn = new MidiInputCtrl();

        //Initialize Logger
        Logger = new LogWriter();
    }

    public bool StartVRMin()
    {
        // Start Up the Logger
        Logger.Start(string.Format("p{0}-session{1}-midi-log", ParticipantID, SessionNum));

        //Start Up the Midi Controllers
        MidiIn.Connect(MidiInputDeviceName);
        MidiIn.Start();

        // Start Playing
        NoteCtrl.Control.MidiScoreFile = MidiScoreResource;
        NoteCtrl.Control.PlayMidi(NoteCtrl.MidiStatus.Play);

        return true;
    }

    public bool StartNewScore()
    {
        NoteCtrl.Control.MidiScoreFile = MidiScoreResource;
        NoteCtrl.Control.PlayMidi(NoteCtrl.MidiStatus.Play);

        Destroy(completedMessage);

        return true;
    }

    public void MidiComplete()
    {   
        //instatiate completed menu and set player controller to this
        completedMenu = Instantiate(completedMenuPrefab);
    }

    void OnDisable()
    {
        if(MidiIn != null)
            MidiIn.StopAndClose();
        if (Logger != null)
            Logger.Stop();
    }
}
