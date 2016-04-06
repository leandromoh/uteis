range(10); // => [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
range(1, 11); // => [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
range(0, 30, 5); // => [0, 5, 10, 15, 20, 25]
range(0, -10, -1); //  => [0, -1, -2, -3, -4, -5, -6, -7, -8, -9]
range(0); // => []

function range(start, limit, step) {
    if(arguments.length == 1) {
        limit = start;
        start = 0;
    }
    if(arguments.length < 3) {
        step = (start < limit) ? 1 : -1;
    }

    var foo = [];

    if(start < limit){
        while(start < limit) {
            foo.push(start);
            start += step;
        }    
    }
    else{
        while(start > limit) {
            foo.push(start);
            start += step;
        }    
    }
        
    return foo;
}
