using System;

namespace TaskProcessor.Communication.Contract {
    public interface IClient<T> where T : IService {
        TResult Invoke<TResult>(Func<T, TResult> func);
    }
}
