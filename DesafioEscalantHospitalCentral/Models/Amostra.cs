using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DesafioEscalantHospitalCentral.Models
{
    public class Amostra
    {
        private readonly string amostra;

        public int NumGenesPaciente => GenesPaciente.Length;
        public int NumGenesVirus => GenesVirus.Length;
        public string GenesPaciente { get; }
        public string GenesVirus { get; }

        public Amostra(string amostra, int index)
        {
            this.amostra = amostra;

            var amostraSplit = amostra.Split(' ');

            if (amostraSplit.Count() != 2)
                throw new Exception($"Amostra {index.ToString()} inválida");

            GenesPaciente = amostraSplit[0];
            GenesVirus = amostraSplit[1];

            if (!IsValid())
                throw new Exception($"Amostra {index.ToString()} inválida");
        }

        //RN3 verifica se possui apenas caracteres [a-z] e um único espaço
        private bool IsValid()
        {
            return
                amostra.IndexOf(" ") > 0 &&
                Regex.IsMatch(GenesPaciente, @"^[a-z]+$") &&
                Regex.IsMatch(GenesVirus, @"^[a-z]+$");
        }
    }
}