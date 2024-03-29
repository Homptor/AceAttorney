using System;
using System.IO;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static List<EvidenceSO> Evidence = new List<EvidenceSO>();
    public static List<EvidenceSO> Profiles = new List<EvidenceSO>();
    public static List<DialogueSO> UsedDialogue = new List<DialogueSO>();
    public static List<TalkSO> UsedTalks = new List<TalkSO>();
    public static List<String> StoryFlags = new List<string>();

    public static Dictionary<string, ArrayList> LoadTSV(string file) {
        
        Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();
        ArrayList list = new ArrayList();

        using (var reader = new StreamReader(Application.dataPath + "/files/" + file + ".tsv")) {
            while (!reader.EndOfStream)
            {
                list = new ArrayList();
                var line = reader.ReadLine();
                if (line == null) continue;
                string[] values = line.Split('	');
                for (int i=1; i < values.Length; i++) {
                    list.Add(values[i]);
                }
                if (values[0] != "") dictionary.Add(values[0], list);
            }
        }

        return dictionary;
    }
}
