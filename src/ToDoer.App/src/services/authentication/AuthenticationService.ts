import { JsonPostRequest } from 'made-networking';
import Emitter from '../../core/messaging/Emitter';
import axiosClient from '../../core/networking/AxiosClient';
import { getCookie } from '../../core/utils/cookies';

class AuthenticationService {
  authenticationApiVersion = '1.0';

  endpoints = {
    authentication: {
      login: 'api/auth/login',
      logout: 'api/auth/logout',
    }
  }

  hasAuthCookie() {
    return getCookie('.ToDoerAuth');
  }

  async login(email: string, password: string) {
    const result = await new JsonPostRequest(axiosClient, this.endpoints.authentication.login, { email, password }, { ApiVersion: this.authenticationApiVersion }).execute();

    if (result.isSuccessStatusCode) {
      Emitter.$emit('logged-in');
    }

    return result;
  }

  async logout() {
    const result = await new JsonPostRequest(axiosClient, this.endpoints.authentication.logout, {}, { ApiVersion: this.authenticationApiVersion }).execute();

    if (result.isSuccessStatusCode) {
      Emitter.$emit('logged-out');
    }

    return result;
  }
}

const authenticationService = new AuthenticationService();
export default authenticationService;
