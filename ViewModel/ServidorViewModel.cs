﻿namespace VideoMonitoramento.APIRest.ViewModel
{
    public class ServidorViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string EnderecoIP { get; set; }

        public int PortaIP { get; set; }
        public string Status { get; set; }

    }
}
