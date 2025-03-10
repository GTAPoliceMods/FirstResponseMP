fx_version 'cerulean'
game 'gta5'

author 'GTAPoliceMods DevOps'
description 'First Response MP Gamemode'
version '1.0.0'

-- ui_page 'https://frmp-ui.gtapolicemods.com/mdt/index.html'
client_script 'FirstResponseMP.Client/FirstResponseMP.Client.net.dll'
server_script 'FirstResponseMP.Server/FirstResponseMP.Server.net.dll'
shared_script 'FirstResponseMP.Shared/FirstResponseMP.Shared.net.dll'

files {
    'FirstResponseMP.Client/**/*',
    'FirstResponseMP.Callouts/**/*',
    'FirstResponseMP.Shared/**/*',
    'FirstResponseMP.Localization/**/*',
	'FirstResponseMP.Config/**/*'
}