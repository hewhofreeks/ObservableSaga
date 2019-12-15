// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


$(document).ready(($) => {
    $('#add').on('click', (e) => {
        $.post('/Counter/Add?counterID=new-id');
    });

    $('#subtract').on('click', (e) => {
        $.post('/Counter/Subtract?counterID=new-id');
    });
});