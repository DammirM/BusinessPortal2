namespace BusinessPortal2.Services
{
    public interface IPersonRepo
    {
        public Task Register();
        public Task Login();
        public Task Delete();
    }
}
