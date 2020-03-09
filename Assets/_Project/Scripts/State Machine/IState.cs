public interface IState
{
    void Initialize();
    void Update();
    void FixedUpdate();
    void Exit();
}