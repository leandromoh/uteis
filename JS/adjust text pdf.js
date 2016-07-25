var body = document.body;
body.style = 'font-size: 17.5px; font-family: Calibri;';

body.innerHTML = body.innerHTML
.replace(/([\w-])\n/g, function(all, g){ return g == '-' ? '' : g + ' '; })
.replace(/[“”]/g,'"')
.replace(/■/g,'#')
.replace(/#\s?#/g,'#')
.replace(/\n/g, "<br><br>")
.replace(/(^(<br>)+|(<br>)+$)/g, '')
.trim();
