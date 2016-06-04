/*
    Diferença básica


    const = quando é declarada precisa já receber um valor.
    readonly = podem ser declaradas sem que um valor seja atribuído. Assim que um valor é atribuído, não se pode muda-lo.

    Diferença Aprofundada

         
    Constants
    + Constants are static by default
    + They must have a value at compilation-time (you can have e.g. 3.14 * 2, but cannot call methods)
    + Could be declared within functions
    + Are copied into every assembly that uses them (every assembly gets a local copy of values)
    + Can be used in attributes
    + Can't be anything except value (primitive) types

    Readonly instance fields
    + Must have set value, by the time constructor exits (and thus have a different value depending on the constructor used).
    + Are evaluated when instance is created

    Static readonly fields
    + Are evaluated when code execution hits class reference (when new instance is created or a static method is executed)
    + Must have an evaluated value by the time the static constructor is done
    + It's not recommended to put ThreadStaticAttribute on these (static constructors will be executed in one thread only and will set the value for its thread; all other threads will have this value uninitialized)
*/