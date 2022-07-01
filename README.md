# World Cup 2018

C# Windows Form Application and C# WPF .NET application technology used.

## Description

Application that allows users to choose basic settings (language, resolution, gender) that are saved in .txt file. 
User chooses his favorite national team that has played World Cup in 2018.
Based on that choice, user can see the team matches, statistics, print players and matches statistics, see formation etc...

## Windows Forms

In the Windows form part, use can set the language, gender and favortite national team settings that are saved in .txt file on solution level.
User has to choose his favorite players and is allowed to change their picture using IO technology that copies file on solution level.
Forms display asynchronous players list of that nation.
Forms also display asynchronous match and player list, in which list are sorted by goals, yellow cards and matches are sorted by attendance.
User can also print the those lists.
At almost any moment, user can change settings which are immediately applied.

## Windows Presentation Foundation WPF

In the WPF part, user can again set the settings with addition of setting the window resulution.
User is presented with WPF form which asynchronously loads the favorite nation if exists, if not, user chooses his favorite nation, which is necessary
because then he is represented with list of nationals teams with whom the favorite nation has played.
Choosing his opponent, displays the final score of that match.
Next to the chosen nation list are buttons that display match statistics for that nation in that match.
When choosing his opponent, to user is display a button which allows you to see the starting eleven, which are shown in the formation
in which they have played the match, with the images that user have previously saved in Windows Form form (Favorite Players form).
Double click on the player, and the player statistics for that match is opened.

## API

To make this possible, this application uses 
### World Cup API: http://worldcup.sfg.io/ - by: 

## Credits

The whole project was made by Arjan Jakupi



