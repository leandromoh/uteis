range(10); // => [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
range(1, 11); // => [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
range(0, 30, 5); // => [0, 5, 10, 15, 20, 25]
range(0, -10, -1); //  => [0, -1, -2, -3, -4, -5, -6, -7, -8, -9]
range(0); // => []

function range(start, count, step) {
    if(arguments.length == 1) {
        count = start;
        start = 0;
    }
    if(arguments.length < 3) {
       if (arguments.length == 1) 
          step = 1;
       else
          step = (start < limit) ? 1 : -1;
    }

    var func = (start < count) ? function(a, b) { return a < b; }
                               : function(a, b) { return a > b; }
    var foo = [];

    while(func(start, count)) {
        foo.push(start);
        start += step;
    }

    return foo;
}
