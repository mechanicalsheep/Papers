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
        public string oldName, oldGroup, oldNote, oldMachineNote, oldOS, oldRam, oldDateTime;
        

        public List<string> softwareAdded, softwareRemoved;

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
            oldDateTime = savedComputer.dateTime;
            dateTime = newComputer.dateTime;

            if (savedComputer.name != newComputer.name)
            {
                name = newComputer.name;
                oldName = savedComputer.name;
            }


            if (savedComputer.group != newComputer.group)
            {
                group = newComputer.group;
                oldGroup = savedComputer.group;
            }

            if (savedComputer.note != newComputer.note)
                note = newComputer.note;

            if (savedComputer.machineNote != newComputer.machineNote)
            {
                machineNote = newComputer.machineNote;
                oldMachineNote = savedComputer.machineNote;
            }

            if (savedComputer.OS != newComputer.OS)
            {
                OS = newComputer.OS;
                oldOS = savedComputer.OS;
            }

            if (savedComputer.ram != newComputer.ram)
            {
                ram = newComputer.ram;
                oldRam = savedComputer.ram;
            }

            if (savedComputer.softwares.Count > newComputer.softwares.Count)
            {
                foreach (var soft in savedComputer.softwares)

                {
                    if (!newComputer.softwares.Contains(soft))
                    {
                        softwareRemoved.Add(soft);
                    }
                }
            }
            if (savedComputer.softwares.Count < newComputer.softwares.Count)
            {
                foreach (var soft in newComputer.softwares)

                {
                    if (!savedComputer.softwares.Contains(soft))
                    {
                        softwareAdded.Add(soft);
                    }
                }
            }
            
        }
    }
}
