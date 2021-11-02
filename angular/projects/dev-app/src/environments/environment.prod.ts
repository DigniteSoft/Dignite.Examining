import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Examining',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44312',
    redirectUri: baseUrl,
    clientId: 'Examining_App',
    responseType: 'code',
    scope: 'offline_access Examining',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44312',
      rootNamespace: 'Dignite.Examining',
    },
    Examining: {
      url: 'https://localhost:44394',
      rootNamespace: 'Dignite.Examining',
    },
  },
} as Environment;
