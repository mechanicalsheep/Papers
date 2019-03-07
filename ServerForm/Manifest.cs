using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForm
{
    
    class Manifest
    {
        Computer savedComputer, newComputer;
     
        public string name, group, note, machineNote, OS, ram, dateTime;
        

        List<string> softwareAdded, softwareRemoved;

        public Manifest(Computer Savedcomputer, Computer NewComputer)
        {
            savedComputer = Savedcomputer;
            newComputer = NewComputer;
            softwareAdded = new List<string>();
            softwareRemoved = new List<string>();

           WhatChanged();
           // Console.WriteLine("Manifest computer dateTime= " + manifestComputer.dateTime);
        }
        public void WhatChanged()
        {
            //Console.WriteLine("entered whatChanged()");
           // Computer computer = new Computer();
            dateTime = newComputer.dateTime;

            if (savedComputer.name != newComputer.name)
                name = newComputer.name;

            if (savedComputer.group != newComputer.group)
                group = newComputer.group;

            if (savedComputer.note != newComputer.note)
                note = newComputer.note;

            if (savedComputer.machineNote != newComputer.machineNote)
                machineNote = newComputer.machineNote;

            if (savedComputer.OS != newComputer.OS)
                OS = newComputer.OS;

            if (savedComputer.ram != newComputer.ram)
                ram = newComputer.ram;

            if (savedComputer.software.Count > newComputer.software.Count)
            {
                foreach (var soft in savedComputer.software)

                {
                    if (!newComputer.software.Contains(soft))
                    {
                        softwareRemoved.Add(soft);
                    }
                }
            }
            if (savedComputer.software.Count < newComputer.software.Count)
            {
                foreach (var soft in newComputer.software)

                {
                    if (!savedComputer.software.Contains(soft))
                    {
                        softwareAdded.Add(soft);
                    }
                }
            }
            
        }
    }
}
