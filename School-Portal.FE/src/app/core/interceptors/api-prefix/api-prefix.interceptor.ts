import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

export const apiPrefixInterceptor: HttpInterceptorFn = (request, next) => {
  const url = request.url;
  
  if (!/^(http|https):/i.test(request.url)) {
    request = request.clone({
      url: (environment.baseURL ?? "") + url
    });
  };

  return next(request);
};
