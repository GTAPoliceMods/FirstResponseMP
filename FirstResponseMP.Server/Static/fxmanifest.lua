fx_version 'cerulean'
game 'gta5'

author 'GTAPoliceMods Developers'
description 'First Response Multiplayer Gamemode'
version '1.0.0'

ui_page 'FirstResponseMP.Interface/index.html'
client_script 'FirstResponseMP.Client/FirstResponseMP.Client.net.dll'
server_script 'FirstResponseMP.Server/FirstResponseMP.Server.net.dll'
shared_script 'FirstResponseMP.Shared/FirstResponseMP.Shared.net.dll'

files {
    'FirstResponseMP.Client/**/*',
    'FirstResponseMP.Shared/**/*',
    'FirstResponseMP.Interface/**/*',
    'FirstResponseMP.Localization/**/*',
	'FirstResponseMP.Config/**/*'
}