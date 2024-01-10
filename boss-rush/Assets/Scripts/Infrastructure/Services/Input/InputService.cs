namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly InputControls _inputControls;

        public InputService()
        {
            _inputControls = new InputControls();
            _inputControls.Enable();
        }

        public InputControls InputControls => _inputControls;
    }
}