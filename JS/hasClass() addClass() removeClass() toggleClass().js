HTMLElement.prototype.hasClass = function(klass){
    return new RegExp('(^|\\s)'+klass+'(\\s|$)').test(this.className);
}

HTMLElement.prototype.removeClass = function(klass){
    this.className = this.className.replace(new RegExp('(^|\\s)'+klass+'(\\s|$)'),' ');
}

HTMLElement.prototype.addClass = function(klass){
    if(!this.hasClass(klass))
        this.className += ' ' + klass;
}

HTMLElement.prototype.toggleClass = function(klass){
    if(this.hasClass(klass))
        this.removeClass(klass);
    else
        this.addClass(klass);
}
