using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Helpers
{
    public class ChatHub : Hub
    {
        public async Task EnviarMensaje(Guid salaId, Guid usuarioSegId, string mensaje)
        {
            await Clients.Group(salaId.ToString()).SendAsync("RecibirMensaje", salaId, usuarioSegId, mensaje);
        }

        public async Task IngresarASala(Guid salaId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, salaId.ToString());
        }

        public async Task SalirDeSala(Guid salaId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, salaId.ToString());
        }
    }
}
