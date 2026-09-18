namespace SSF.PortalBI.DTOs
{
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas => TamanoPagina == 0 ? 0 : (int)Math.Ceiling(TotalRegistros / (double)TamanoPagina);
    }
}