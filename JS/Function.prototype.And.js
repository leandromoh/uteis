Function.prototype.And = function(f) {
    var t = this;
    return function(){
        return t.apply(null, arguments) && f.apply(null, arguments);
    }
}

var f1 = function(i) {
    return i % 2 == 0;
}

var f2 = function(i) {
    return i % 3 == 0;
}

var f3 = function(i) {
    return i > 10;
}

var fx = f1.And(f2).And(f3);

console.log(fx(12));
console.log(fx(6));
