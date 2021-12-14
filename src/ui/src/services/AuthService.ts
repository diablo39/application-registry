/* eslint-disable @typescript-eslint/camelcase */
/* eslint-disable  prefer-rest-params */
import Oidc, {UserManager} from 'oidc-client';

export default class AuthService {
    private mgr: UserManager;

    constructor(config: any) {
        Oidc.Log.logger = console;
        Oidc.Log.level = Oidc.Log.INFO;

        const mgr = new UserManager({
            authority: config.authentication.authority,
            client_id: config.authentication.clientId,
            redirect_uri: config.authentication.redirectUri,
            response_type: config.authentication.responseType,
            scope: config.authentication.scope,
            post_logout_redirect_uri: config.authentication.postLogoutRedirectUri,
            userStore: new Oidc.WebStorageStateStore({store: window.localStorage}),
            automaticSilentRenew: true,
            silent_redirect_uri: config.authentication.silentRedirectUri,
            loadUserInfo: false,
        });

        this.mgr = mgr;

        mgr.events.addUserLoaded(function (user) {
            console.log('New User Loaded：', arguments);
            console.log('Access_token: ', user.access_token)
        });

        mgr.events.addAccessTokenExpiring(function () {
            console.log('AccessToken Expiring：', arguments);
        });

        mgr.events.addAccessTokenExpired(function () {
            console.log('AccessToken Expired：', arguments);
            alert('Session expired. Going out!');
            mgr.signoutRedirect().then(function (resp) {
                console.log('signed out', resp);
            }).catch(function (err) {
                console.log(err)
            })
        });

        mgr.events.addSilentRenewError(function () {
            console.error('Silent Renew Error：', arguments);
        });

        mgr.events.addUserSignedOut(function () {
            alert('Going out!');
            console.log('UserSignedOut：', arguments);
            mgr.signoutRedirect().then(function (resp) {
                console.log('signed out', resp);
            }).catch(function (err) {
                console.log(err)
            })
        });
    }

    public async getUser() {
        try {
            const result = await this.mgr.getUser();
            return result;
        } catch (err) {
            console.log(err);
        }
        return null;
    }

    public login(returnPath): Promise<void> {
        return returnPath ? (this as any).mgr.signinRedirect({state: returnPath})
            : (this as any).mgr.signinRedirect();
    }

    public logout(): Promise<void> {
        return this.mgr.signoutRedirect();
    }

    public getAccessToken(): Promise<string> {
        return this.mgr.getUser().then((data: any) => {
            return data.access_token;
        });
    }

    public async signinRedirectCallback() {
        return this.mgr.signinRedirectCallback();
    }

    public async signinSilentCallback() {
        return this.mgr.signinSilentCallback();
    }
}