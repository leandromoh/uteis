function range(start, limit, step){
    stepX = (arguments.length < 3) ? (arguments.length == 1) ? 1 : (start < limit) ? 1 : -1 : step;
    return arguments.length == 1 ? rangeY(0, start, stepX) : rangeY(start, limit, stepX);
}

function rangeY(start, limit, step){
    return rangeX(start, limit, step, (start < limit) ? function(a, b) { return a <= b; } : function(a, b) { return a >= b; });
}

function rangeX(start, limit, step, func){
		return func(start, limit) ? [start].concat(rangeX(start+step, limit, step, func)) : [];
}
