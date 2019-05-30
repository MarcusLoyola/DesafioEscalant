using DesafioEscalantHospitalCentral.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioEscalantHospitalCentral.Models
{
    public class Laboratorio
    {
        //Regras de Negócio
        //RN1 - A primeira linha precisa ser um número entre 1 e 10
        //RN2 - O número da primeira linha deve ser igual ao número de amostras
        //RN3 - Cada amostra deve ser composta apenas por caracteres minúsculos [a-z] e um único espaço separando o DNA do vírus
        public string Resultado { get; private set; }

        public void ProcessaAmostra(AmostraPacienteVirusViewModel model)
        {
            try
            {
                IEnumerable<string> entradas = model.AmostraEntrada
                    .Split('\n')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToList();

                if (!int.TryParse(entradas.First(), out int numAmostras))
                    throw new Exception("Número de amostras inválido - Valor não numérico informado na primeira linha.");

                //RN1
                if (numAmostras < 1 || numAmostras > 10)
                    throw new Exception("Número de amostras inválido - Era esperado um valor numérico entre 1 e 10 na primeira linha.");

                int conta = 0;
                var listaAmostras = entradas
                    .Skip(1)
                    .Select(x => new Amostra(x, ++conta))
                    .ToList();

                //RN2
                if (numAmostras != listaAmostras.Count)
                    throw new Exception("Número de amostra incompatível com o informado.");

                Resultado = string.Join("\r\n", listaAmostras.Select(x => AnalisaAmostras(x)));
            }
            catch(Exception ex)
            {
                Resultado = ex.Message;
            }
        }

        public string AnalisaAmostras(Amostra amostra)
        {
            List<int> saida = new List<int>();
            //varia da primeira à última letra dos genes do paciente
            for (var c = 0; c < amostra.NumGenesPaciente - (amostra.NumGenesVirus - 1); c++)
            {
                string avalia = amostra.GenesPaciente.Substring(c, amostra.NumGenesVirus);
                var resultado = ComparaGenes(avalia, amostra.GenesVirus);
                if (resultado)
                    saida.Add(c);
            }
            if (saida.Any())
                return string.Join(" ", saida);
            else
                return "Sem correspondência";
        }

        public bool ComparaGenes(string partePaciente, string parteVirus)
        {
            char[] pac = partePaciente.ToArray();
            char[] vir = parteVirus.ToArray();
            int tamanho = partePaciente.Length;
            int contaIgual = 0;

            for (int i = 0; i < tamanho; i++)
                contaIgual += (pac[i] == vir[i]) ? 1 : 0;

            return contaIgual >= tamanho - 1;
        }
    }   
}