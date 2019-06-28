﻿/// <binding Clean='clean' />
"use strict";
 
var gulp = require("gulp"),
    rimraf = require("rimraf"),
    less = require("gulp-less"),
    sass = require("gulp-sass"); // добавляем модуль sass
 
var paths = {
    webroot: "./wwwroot/"
};
// регистрируем задачу для конвертации файла scss в css
gulp.task("sass", function () {
    return gulp.src('Sass/styles2.scss')
            .pipe(sass())
            .pipe(gulp.dest(paths.webroot + '/css'))
});
