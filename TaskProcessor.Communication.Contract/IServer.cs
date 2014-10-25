using System;

namespace TaskProcessor.Communication.Contract {
    public interface IServer {
        void Start(Uri uri);
    }
}
