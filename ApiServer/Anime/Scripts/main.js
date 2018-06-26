function DeleteFilm(id, el) {

    
    let element = '#SelectedFilm-' + id;
    let element2 = '#SelectedFilm-' + id + '-Search';
    let isDelete = confirm('Удалить выбраный элемент?');
    if (!isDelete) {
        return;
    }
    if (isDelete) {
        document.getElementById('loading').style.display = 'flex';

        $.ajax({
            url: "Manage/DeleteMovie",
            data: {
                id: id
            },
            success: function () {
                $(element).hide();
                $(element2).hide();
                $('.loading').hide();
            },
            error: function () {
                $('.loading').hide();
                alert('error');
            }
        });
    }
}

$(document).ready(function () {
    $('#search').on('input', () => {
        let str = $('#search').val();
        if (str.length >= 3) {
            $.ajax({
                url: "Movies/GetFilmsByName",
                data: {
                    n: str
                },
                success: function (res) {
                    $('#h-search').show();
                    $('#search-result-div').empty();
                    $('.total-find').text(res.length);
                    $.each(res, (i, v) => {
                        console.log(v);
                        $('#search-result-div').append(`<div class="col-md-12 container-movie" id='SelectedFilm-${v.FilmId}-Search'>
                                                            <span>${v.Name}</span>
                                                            <div class="btn-manage">
                                                                <button onclick="DeleteFilm(${v.FilmId}, this)" class="glyphicon glyphicon-remove-circle"></button>
                                                                <a href="/Manage/EditMovie/?id=${v.FilmId}" class="glyphicon glyphicon-edit"></a>
                                                            </div>
                                                        </div>`);
                    })
                }
            });
        }
        if (str.length == 0) {
            $('#h-search').hide();
            $('#search-result-div').empty();
        }
    })

    $('#hide-result').click(() => {
        $('#search-result-div').empty();
    });

    $('#search').blur(() => {
        if ($('#search').val().length == 0) {
            $('#h-search').hide();
            $('#search-result-div').empty();
        }
    })

});