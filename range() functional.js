function range(start, limit, step){
    var stepX = getStep(arguments);
                
    return arguments.length == 1 
           ? rangeY(0, start, stepX) 
           : rangeY(start, limit, stepX);
}

function rangeY(start, limit, step){
    var func = start < limit
               ? function(a, b) { return a <= b; } 
               : function(a, b) { return a >= b; };
               
    return rangeX(start, limit, step, func);
}

function rangeX(start, limit, step, func){
    return func(start, limit) 
           ? [start].concat(rangeX(start+step, limit, step, func)) 
           : [];
}

function getStep(params)
{
    return (params.length < 3) 
           ?  (params.length == 1)
              ?  (0 < params[0])
                 ? 1 
                 : -1
              :  (params[0] < params[1])
                 ? 1 
                 : -1
           : params[2];
}
