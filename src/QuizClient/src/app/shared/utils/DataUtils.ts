import { environment } from '../../../environments/environment';

/** Base API url. Does not contain trailing slash. */
export const BaseApiUrl = `${environment.apiProtocol}://${window.location.hostname}:${environment.apiPort}/api`;
