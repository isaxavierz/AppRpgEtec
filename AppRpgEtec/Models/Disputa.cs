﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AppRpgEtec.Models
{
    public class Disputa
    {
        public int Id { get; set; } 
        public DateTime? DataDisputa { get; set; }
        public int AtacanteId { get; set; }
        public int OponenteId { get; set; }
        public string Narracao { get; set; }
        public int HabilidadeId {  get; set; }
        public List<int> ListaIdPersonagens { get; set; } = new List<int>();
        public List<string> Resultados { get; set; } = new List<string>();
    }

}
