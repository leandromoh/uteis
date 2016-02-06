<textarea id="asd" cols="50" rows="10" ></textarea>

<script>
	// font: http://jsfiddle.net/joaocolombo/TQYmB/3/

	HTMLTextAreaElement.prototype.getCaretPosition = function() {
		return this.selectionStart;
	};
	HTMLTextAreaElement.prototype.setCaretPosition = function(position) {
		this.selectionStart = position;
		this.selectionEnd = position;
		this.focus();
	};
	HTMLTextAreaElement.prototype.hasSelection = function() {
		if (this.selectionStart == this.selectionEnd) {
			return false;
		} else {
			return true;
		}
	};
	HTMLTextAreaElement.prototype.getSelectedText = function() {
		return this.value.substring(this.selectionStart, this.selectionEnd);
	};
	HTMLTextAreaElement.prototype.setSelection = function(start, end) {
		this.selectionStart = start;
		this.selectionEnd = end;
		this.focus();
	};
    
    document.getElementById("asd").onkeydown = function(event) {
        
        //support tab on this
        if (event.keyCode == 9) { //tab was pressed
            var newCaretPosition;
            newCaretPosition = this.getCaretPosition() + "    ".length;
            this.value = this.value.substring(0, this.getCaretPosition()) + "    " + this.value.substring(this.getCaretPosition(), this.value.length);
            this.setCaretPosition(newCaretPosition);
            return false;
        }
        if(event.keyCode == 8){ //backspace
            if (this.value.substring(this.getCaretPosition() - 4, this.getCaretPosition()) == "    ") { //it's a tab space
                var newCaretPosition;
                newCaretPosition = this.getCaretPosition() - 3;
                this.value = this.value.substring(0, this.getCaretPosition() - 3) + this.value.substring(this.getCaretPosition(), this.value.length);
                this.setCaretPosition(newCaretPosition);
            }
        }
        if(event.keyCode == 37){ //left arrow
            var newCaretPosition;
            if (this.value.substring(this.getCaretPosition() - 4, this.getCaretPosition()) == "    ") { //it's a tab space
                newCaretPosition = this.getCaretPosition() - 3;
                this.setCaretPosition(newCaretPosition);
            }    
        }
        if(event.keyCode == 39){ //right arrow
            var newCaretPosition;
            if (this.value.substring(this.getCaretPosition() + 4, this.getCaretPosition()) == "    ") { //it's a tab space
                newCaretPosition = this.getCaretPosition() + 3;
                this.setCaretPosition(newCaretPosition);
            }
        } 
    }


</script>
