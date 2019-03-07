using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForm
{
    class Manifest
    {
        Computer manifestComputer, savedComputer, newComputer;
        string computerName;
        string date;
        Dictionary<string, string> change;
        List<string> softwareAdded, softwareRemoved;

        public Manifest(Computer Savedcomputer, Computer NewComputer)
        {
            savedComputer = Savedcomputer;
            newComputer = NewComputer;
            softwareAdded = new List<string>();
            softwareRemoved = new List<string>();

            manifestComputer = WhatChanged();
        }
        public Computer WhatChanged()
        {
            Computer computer = new Computer();
            computer.dateTime = newComputer.dateTime;

            if (savedComputer.name != newComputer.name)
                computer.name = newComputer.name;

            if (savedComputer.group != newComputer.group)
                computer.group = newComputer.group;

            if (savedComputer.note != newComputer.note)
                computer.note = newComputer.note;

            if (savedComputer.OS != newComputer.OS)
                computer.OS = newComputer.OS;

            if (savedComputer.ram != newComputer.ram)
                computer.ram = newComputer.ram;

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
            




            return computer;
        }
    }
}
