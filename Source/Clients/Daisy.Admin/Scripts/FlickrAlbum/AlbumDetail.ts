module Album {
    export interface IPhoto {
        FlickrPhotoId: string;
        Name: string;
        SmallUrl: string;
        MediumUrl: string;
        LargeUrl: string;
        OriginalUrl: string;
    }

    export interface IAlbumDetail {
        Album: IAlbum;
        Photos: IPhoto[]; 
    }

    export class FlickrAlbumDetail {
        static album: IAlbumDetail;
        static importAlbumDetailRequestUrl: string;

        importAlbumDetail(albumDetail: Album.IAlbumDetail) {
            var data = {
                Album: albumDetail.Album,
                Photos: albumDetail.Photos
            };
            $.ajax({
                url: FlickrAlbumDetail.importAlbumDetailRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: data,
                success: (response) => {
                    if (response == "Success") {
                        toastr.success('Import successfully');
                    }
                    else {
                        toastr.error(response);
                    }
                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    console.log('Desc: ' + desc + '\nErr:' + err);
                },
                beforeSend: function () {
                    $('#loader').show();
                },
                complete: function () {
                    $('#loader').hide();
                },
            });
        }
    }
}

$(document).ready(function () {
    var albumDetail = new Album.FlickrAlbumDetail();

    $('#chkSelectAll').change(function () {
        if (this.checked) {
            $('#gridPhotos input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridPhotos input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });
    
    $('#btnImport').click(function () {
        var albumModel = Album.FlickrAlbumDetail.album;
        var importedAlbum: Album.IAlbumDetail = {
            Album: albumModel.Album,
            Photos: []
        };
        
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var flickrPhotoId = $(this).val();            
            var photo = $.grep(albumModel.Photos, function (e) { return e.FlickrPhotoId == flickrPhotoId });
            importedAlbum.Photos.push(photo[0]);
        });

        if (importedAlbum.Photos.length == 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Import album',
                message: 'Please choose photo(s) to import.'
            });
        }
        else {
            Album.FlickrAlbumDetail.importAlbumDetailRequestUrl = $(this).data('request-url');
            albumDetail.importAlbumDetail(importedAlbum);
        }
    });
});  

