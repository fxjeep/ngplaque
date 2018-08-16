import { HashRouter } from 'react-router-dom';
import Config from "../config";

export default function request (method, url, body) {
  method = method.toUpperCase();
  if (method === 'GET') {
    // fetch的GET不允许有body，参数只能放在url中
    body = undefined;
  } else {
    body = JSON.stringify(body);
  }

  let rawResp;

  return fetch(url, {method,
                headers: {
                  'Content-Type': 'application/json',
                  'Accept': 'application/json',
                  "Access-Control-Allow-Origin": "*",
                  "Access-Control-Allow-Methods":"POST, GET, PUT, DELETE, OPTIONS",
                  'Access-Token': sessionStorage.getItem(Config.SessionKey) || '' // 从sessionStorage中获取access token
                },
                body
    })
    .then(response => {
      rawResp = response;
      return response.json();})
    .then((respjson) => {
      if (rawResp.status === 401) {
        HashRouter.push('/login');
        return Promise.reject('Unauthorized.');
      }
      else if (rawResp.status === 422){
        return Promise.reject(respjson.error);
      }
      else {
        const token = rawResp.headers.get(Config.SessionKey);
        if (token) {
          sessionStorage.setItem(Config.SessionKey, token);
        }
        return respjson;
      }
    });
}

export const get = url => request('GET', url);
export const post = (url, body) => request('POST', url, body);
export const put = (url, body) => request('PUT', url, body);
export const del = (url, body) => request('DELETE', url, body);