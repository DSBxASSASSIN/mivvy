using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandbase {
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    public string commandId { get { return _commandId;  } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat { get { return _commandFormat; } }

    public DebugCommandbase(string id, string description, string format) {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}   

public class DebugCommand : DebugCommandbase {
    private Action command;
    public DebugCommand(string id, string description, string format, Action command) : base (id, description, format) {
        this.command = command;
    }

    public void Invoke() {
        command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandbase {
    private Action<T1> command;

    public DebugCommand(string id, string description, string format, Action<T1> command) : base(id, description, format) {
        this.command = command;
    }

    public void Invoke(T1 v1) {
        command.Invoke(v1);
    }
}

public class DebugCommand<T1, T2> : DebugCommandbase {
    private Action<T1, T2> command;

    public DebugCommand(string id, string description, string format, Action<T1, T2> command) : base(id, description, format) {
        this.command = command;
    }

    public void Invoke(T1 v1, T2 v2) {
        command.Invoke(v1, v2);
    }
}

public class DebugCommand<T1, T2, T3> : DebugCommandbase {
    private Action<T1, T2, T3> command;

    public DebugCommand(string id, string description, string format, Action<T1, T2, T3> command) : base(id, description, format) {
        this.command = command;
    }

    public void Invoke(T1 v1, T2 v2, T3 v3) {
        command.Invoke(v1, v2, v3);
    }
}