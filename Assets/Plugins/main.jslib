mergeInto(LibraryManager.library, {
    SaveExternal: function(fieldName, data) {
        var dataString = UTF8ToString(data);
        localStorage.setItem('aidiffs-data/' + UTF8ToString(fieldName), dataString);
    },
    GetSerializedExternal: function(fieldName) {
       SendMessage("Save", "SetSerializedData", localStorage.getItem('aidiffs-data/' + UTF8ToString(fieldName)) || '');
    },
    
    ShowAdExternal: function() {
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                    SendMessage("Ads", "InvokeAdWatched");
                },
                onError: function(error) {
                    SendMessage("Ads", "InvokeAdWatched");
                }
            }
        })
    },
    
    ShowRewardedExternal: function() {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                  console.log("Rewarded Open");
                },
                onRewarded: () => {
                  SendMessage("Ads", "InvokeRewardedWatched");
                },
                onClose: () => {
                  SendMessage("Ads", "InvokeRewardedSkipped");
                }, 
                onError: (e) => {
                  console.log('Error while open video ad:', e);
                }
            }
        })
    }
});