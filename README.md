# PhotoExhibiter

Social network where users showcase their photography exhibits and attend other
users' exhibits. Admin users can manage the site content and users.

Architecture is vertically sliced, CQRS, with a rich, encapsulated
domain<sup>1</sup> (private collections and setters). The errors are handled
with command results (similar to F#'s Option Type or Haskell's Maybe monad).

1. The only exception to this (as far as I'm aware) is the ApplicationUser
   class which references Identity Framework.

Technology
----------
* ASP.NET Core 2.0
* Identity 2.0
* MySQL
* Entity Framework Core 2.0 
* MediatR
* FluentValidation
* NLog
* CSharpFunctionalExtensions
* Bootstrap 4 (with Font Awesome)
* DataTables
* Noty
* Moment
* Rellax
* Google Maps API

Screenshots
---
### Main  
Display all upcoming exhibits from all photographers and get notified when any
of your attending exhibit details changes or gets cancelled.
![main](/screenshots/main.png?raw=true "Main")
### Exhibits
Create exhibits so that others can attend.
![exhibit](/screenshots/exhibit.png?raw=true "Exhibit")
### Profile
The default profile image is a gravatar but users have the ability to upload
a photo. Next to the profile image are your upcoming exhibits, attending
exhibits, followers, and followings (switch content with tabs).
![profile](/screenshots/profile.png?raw=true "Profile")
****
![attending](/screenshots/attending.png?raw=true "Attending")
****
![followings](/screenshots/followings.png?raw=true "Followers")

### Admin 
Admin users may manage the exhibits and application users with abilities to cancel exhibits and suspend users.
![admin_exhibits](/screenshots/manage_exhibits.png?raw=true "Admin")
***
![admin_users](/screenshots/manage_users.png?raw=true "AdminUsers")
***

Run
---
You will need webpack. If you have webpack installed, then go ahead and install
the javascript dependencies (e.g. `npm install`).
```
docker-compose build
docker-compose up
Go to http://localhost:5000
```
Alternatively, If you have the ASP.NET Core 2.0 SDK installed,
then open `appsettings.Development.json` and point the connection strings to
your MySQL server. Install the javascript dependencies (e.g.
`npm install`). You will also need the weback-cli to build the frontend.

```
npm run build
dotnet restore
dotnet ef database update 
dotnet run
Go to http://localhost:5000
```

Deploy (Dockerized hosts)
---
This process is more thoroughly explained
[here](https://www.digitalocean.com/community/tutorials/how-to-provision-and-manage-remote-docker-hosts-with-docker-machine-on-ubuntu-16-04),
but I'll summarize the steps required (mostly for my own reference).

Prerequisites: Docker Machine installed on your local machine and DigitalOcean
API token.

1. Create Dockerized hos
```
docker-machine create --driver digitalocean --digitalocean-access-token
$DOTOKEN machine-name
```
2. Activate Dockerized host
```
eval (docker-machine env machine-name)
```
3. Build and run containers
```
docker-compose -f docker-compose.yml -f docker-compose.prod.yml build
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d
```
4. Unset Dockerized host
```
eval (docker-machine env -u)
```

NOTE
----

The resources I use to create this project were plentiful, coming from several
projects and tutorials provided by Microsoft (mostly eShopOnWeb,
eShopOnContainers, MVCMusicStore, and ContosoUniversity), Pluralsight, Jimmy
Bogards Contoso University remake, and several blogs.

TODO
----
Optimize Dockerfile to reduce build time/size<br>
Prune npm dependencies<br>
Add pagination to the profiles page<br>
Add social Auth
