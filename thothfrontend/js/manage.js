const api = "https://thothwebapp.azurewebsites.net/manage/quoteitem";

$(document).ready(function(){
    $(".shownew").click(shownew);
    $(".showall").click(showall);
    $(".addrow").click(addnewrow);
    $(".butomSave").click(savechanges);
    refreshview(true);                            
    });

    function shownew()
    {
        $("#recordsTable tr").slice(1).remove();
        $(".showall").removeClass('active');
        $(".shownew").addClass('active');
        refreshview(true);
    };

    function showall()
    {
        $("#recordsTable tr").slice(1).remove();
        $(".shownew").removeClass('active');
        $(".showall").addClass('active');
        refreshview(false);
    }

    function addnewrow(){
        $("#id").prop( "disabled", true );
        $("#postTime").prop( "disabled", true );
        $('#quoteModal').modal();   
    };

    function savechanges()
        {
            var $id = $("#quoteModal").data("id"); 
            if($id=="")
            {
                $.ajax({
                    url: api,
                    type: 'post',
                    crossdomain: true,
                    contentType: 'application/json',
                    dataType: 'json',
                    processData: false,
                    data: JSON.stringify(objectifyForm($('#editform').serializeArray())),
                    success: function(data) {
                        $('#quoteModal').modal('hide');
                            alert("New item was added sucefully!")
                            var row = '<tr id= "'+ data.id +'"><td scope="row">' + data.id + '</td>' +
                                        '<td>' + data.primaryText + '</td>' +
                                            '<td>' + data.secondaryText + '</td>' +
                                                '<td>' + data.author + '</td>' +
                                                    '<td>' + data.postTime + '</td>' +
                                                        '<td>' + data.isVisible + '</td>' +
                                                            '<td> <button type="button" onclick="editrow(this)" data-id ="' + data.id +  '"class="btn btn-outline-primary editrow">Edit</button></td>' +
                                                            '<td> <button type="button" onclick="deleterow(this)" data-id ="' + data.id +  '"class="btn btn-outline-danger deleterow">Delete</button></td>' +  
                                                    '</tr>';
                            $('#recordsTable tr:first').after(row); 
                        },
                        done: function()
                        {
                            $('#quoteModal').modal('hide');   
                        }
                        });
            }
            else
            {
                $.ajax({
                    url: api+"/"+$id,
                    type: 'put',
                    crossdomain: true,
                    contentType: 'application/json',
                    dataType: 'json',
                    processData: false,
                    data: JSON.stringify(objectifyForm($("#editform").serializeArray())),
                    success: function(data) {
                        $('#quoteModal').modal('hide');
                            $('#' + $id).html(
                                '<td>' + data.id + '</td>' +
                                    '<td>' + data.primaryText + '</td>' +
                                        '<td>' + data.secondaryText + '</td>' +
                                            '<td>' + data.author + '</td>' +
                                                '<td>' + data.postTime + '</td>' +
                                                     '<td>' + data.isVisible + '</td>' +
                                                        '<td> <button type="button" onclick="editrow(this)" data-id ="' + data.id +  '"class="btn btn-outline-primary editrow">Edit</button></td>' +
                                                        '<td> <button type="button" onclick="deleterow(this)" data-id ="' + data.id +  '"class="btn btn-outline-danger deleterow">Delete</button></td>'

                            )
                        },
                        done: function()
                        {
                            $('#quoteModal').modal('hide');   
                        }
                        });
            }
        };

    function refreshview(shownew){
        $link = api+"?/NewOnly="+shownew;
        $.getJSON($link, {
      
        }).done(function(data, text, request){
               
            populatepagination(request.getResponseHeader('x-pagination'));    
            
            data.forEach(record => addrow(record));
            }) 
    };

    function populatepagination(header)
    {
        
    };

    function editrow(obj){
        var $id = $(obj).data("id"); 
        $.getJSON(api+"/"+$id, {
        }).done(function(data){
            $("#id").val(data.id);
            $('#quoteModal').data("id", data.id);  
            $("#primaryText").val(data.primaryText);
            $("#secondaryText").val(data.secondaryText);
            $("#author").val(data.author);
            $("#postTime").val(data.postTime);
            $("#isVisible").attr('checked',data.isVisible);
            $('#quoteModal').modal();   
        }) 
    };

    function deleterow(obj)
    {
        var $id = $(obj).data("id");
        $.ajax({
            url: api+"/"+$id,
            type: 'delete',
            crossdomain: true,
            success: function(data) {
                $('#' + $id).remove();
                alert("Delete sucefully!")
            },
    }); 
    };


    function alert(message){
        $('#alert').html('<div class="alert alert-secondary"><a class="close" data-dismiss="alert">×</a><span>'+message+'</span></div>');
        $('.alert').alert()
    };

    function addrow(data){
        var row = '<tr id= "'+ data.id +'"><td scope="row">' + data.id + '</td>' +
                    '<td>' + data.primaryText + '</td>' +
                        '<td>' + data.secondaryText + '</td>' +
                            '<td>' + data.author + '</td>' +
                                '<td>' + data.postTime + '</td>' +
                                    '<td>' + data.isVisible + '</td>' +
                                        '<td> <button type="button" onclick="editrow(this)" data-id ="' + data.id +  '"class="btn btn-outline-primary editrow">Edit</button></td>' +
                                        '<td> <button type="button" onclick="deleterow(this)" data-id ="' + data.id +  '"class="btn btn-outline-danger deleterow">Delete</button></td>' +  
                                  '</tr>';
            $('#recordsTable tr:last').after(row); 
                                        
    };

    function objectifyForm(formArray) {
        var returnArray = {};
        for (var i = 0; i < formArray.length; i++)
        {
           if(!formArray[i].disabled)
                returnArray[formArray[i]['name']] = formArray[i]['value'];
        }
        return returnArray;
    };








