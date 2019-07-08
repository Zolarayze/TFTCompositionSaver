# Teamfight Tactics Composition Saver
Did you ever find a specific team composition in Teamfight Tactics which worked really well? You can now recreate and save your favourite team compositions with Teamfight Tactics Composition Saver!

## The basics
There are two modes within the application: read and edit mode.
You can switch between them by using the button at the top right (the eye or the pencil with the note).
In read mode you can only view the compositions and in edit mode you can add, remove and edit them.

Read mode:

![alt text](https://puu.sh/DQey0.png "Read mode")

Edit mode:

![alt text](https://puu.sh/DQeyS.png "Edit mode")


## Compositions:
You can add new compositions and remove existing compositions in edit mode.

![alt text](https://puu.sh/DQetw.png "Compositions")

You can edit the title of the composition at the top when the input box is visible.

![alt text](https://puu.sh/DQex8.png "Edit composition title")

## Adding champions
You can add a new champion row to your composition by clicking the plus button.

![alt text](https://puu.sh/DQeF7.png "Add champion")

After you click on this, you get a new row with a lot of plus buttons. You can click on the big plus button to choose a champion and the other plus buttons are there to pick the items that you want the champion to build. You can remove the champion row again by clicking the garbage button at the far left. 

![alt text](https://puu.sh/DQeH5.png "Plus buttons")

### Traits and ordering
After you choose a champion, their traits will appear to the left of the champion's icon. 
You can move the row up or down in order by clicking the up and down arrow buttons.

![alt text](https://puu.sh/DQeLa.png "Ordering rows")

### Choosing the champion's level
You can show which champion you want to fully stack, one way to do this is by showing the amount of levels the champion should aim for. This can be done by clicking the stars next to the champion.

![alt text](https://puu.sh/DQeDn.png "Champion level")

### Items
You can choose from basic and completed items. To make it easier for you to know what items you need to build to get a certain completed item, you can first add two basic items and then add the finalized item as a third item (see the image below).

![alt text](https://puu.sh/DQepM.png "Layout of items added")

### Swapping champions out
Sometimes you can only get certain champions in your composition, while better alternatives are available. These champions can later be swapped if you find that better alternative. To show this, you can click the red icon with an 'S' in it. If the icon is clicked on when it was somewhat translucent, the icon will become fully opaque and will be visible in read mode.

![alt text](https://puu.sh/DQeTG.png "Layout of items added")

# F.A.Q.
1. How to download/install?
   - Go to releases (https://github.com/Zolarayze/TFTCompositionSaver/releases) and download the TFT Composition Saver executable. After    downloading you can place it anywhere you want and run it.

2. Where is the data saved?
   - The data is saved at "%appdata%\TFTCompositionSaver"

3. When I open the application, all the images are gone and replaced with text!
   - Images are downloaded the first time you open the application. Once downloaded, the images will be loaded in the next time the        application is started. If the application is missing certain images, they will be downloaded when the application is started.

   They are downloaded from the following websites:
     - http://raw.communitydragon.org/latest/game
     - https://cdn.communitydragon.org/latest/champion/74/square.png (the number is different for each champion)

   This means that if these websites are down, the images aren't downloaded.
   If this is the case, you can download the images manually by downloading the .zip-file in the repository under the folder "Images".

   Extract the .ZIP-file in the "%appdata%\TFTCompositionSaver" directory, so that the structure is like
     * "%appdata%\TFTCompositionSaver\Champions"
     * "%appdata%\TFTCompositionSaver\Data"
     * "%appdata%\TFTCompositionSaver\Items"
     * "%appdata%\TFTCompositionSaver\Traits"



