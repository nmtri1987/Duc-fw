﻿// CREATE SNIPPETS
var $ = function (id) { return document.getElementById(id); }
var _ = function (tag) { return document.getElementsByTagName(tag); }

// THE MINIFIER & MAXIFIER
function minify_maxify(type, input, output) {

    _('p')[0].innerHTML = '';
    _('p')[0].style.padding = 0;

    if (type === 'Minify HTML') {
        output.value = input.value
          .replace(/\<\!--\s*?[^\s?\[][\s\S]*?--\>/g, '')
          .replace(/\>\s*\</g, '><')
        ;
    }
    else if (type === 'Minify CSS') {
        output.value = input.value
        .replace(/\/\*.*\*\/|\/\*[\s\S]*?\*\/|\n|\t|\v|\s{2,}/g, '')
        .replace(/\s*\{\s*/g, '{')
        .replace(/\s*\}\s*/g, '}')
        .replace(/\s*\:\s*/g, ':')
        .replace(/\s*\;\s*/g, ';')
        .replace(/\s*\,\s*/g, ',')
        .replace(/\s*\~\s*/g, '~')
        .replace(/\s*\>\s*/g, '>')
        .replace(/\s*\+\s*/g, '+')
        .replace(/\s*\!\s*/g, '!')
        ;
    }
    else if (type === 'Minify JS') {
        output.value = input.value
          .replace(/\/\*[\s\S]*?\*\/|\/\/.*\n|\s{2,}|\n|\t|\v|\s(?=function\(.*?\))|\s(?=\=)|\s(?=\{)/g, '')
          .replace(/\s?function\s?\(/g, 'function(')
          .replace(/\s?\{\s?/g, '{')
          .replace(/\s?\}\s?/g, '}')
          .replace(/\,\s?/g, ',')
          .replace(/if\s?/g, 'if')
        ;
        notice("NOTE: Though this JS Minifier is functional, the minified scripts won't run since I have variables and functions inline instead of on a new line...so apparently I'm missing something; because, real JS minifiers can put everything on a newline and still work.\n\nAlso, this doesn't NOT replace your function names and variables with alphabet letters to farther its minification.", '#ff8800', '#fff');
    }
    else if (type === 'Maxify JS') {
        input.value = output.value
          .replace(/\{/g, ' {\n\t')
          .replace(/\}/g, '\n}\n')
          .replace(/\;/g, ';\n\t')
        ;
        notice("Not usable, especially with like RegEx characters! Bummer dude.", '#ff8800', '#fff');
    }
    else if (type === 'Maxify CSS') {
        input.value = output.value
          .replace(/\,/g, ', ')
          .replace(/\{/g, ' {\n\t')
          .replace(/\}/g, '}\n')
          .replace(/\;/g, ';\n\t')
        ;
        notice("Well, it's decent; comparatively.", '#008800', '#fff');
    }
    else if (type === 'Maxify HTML') {
        input.value = output.value
          .replace(/\>\</g, '>\n<');
        notice("I know, no indenting; yet...", '#008888', '#fff');
    }
    else { input.value = ''; output.value = ''; }
}

function notice(message, background, color) {
    _('p')[0].innerHTML = message;
    _('p')[0].style.cssText = 'padding:10px;background:' + background + ';color:' + color + ';';
}

// NO ID'S TO REMEMBER THE NON-JQUERY WAY OF HANDLING TAGS OR CLASSES, AND FOR UNREASONABLY SUPER SIMPLE HTML
var buttons = _('button');
for (var i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', function () {
        minify_maxify(
          this.innerHTML, _('textarea')[0],
          _('textarea')[1]
        );
    }, false);
}
