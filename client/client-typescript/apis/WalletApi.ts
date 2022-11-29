/* tslint:disable */
/* eslint-disable */
/**
 * ZPI API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: all
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  FullWalletDto,
  ProblemDetails,
  WalletDto,
} from '../models';
import {
    FullWalletDtoFromJSON,
    FullWalletDtoToJSON,
    ProblemDetailsFromJSON,
    ProblemDetailsToJSON,
    WalletDtoFromJSON,
    WalletDtoToJSON,
} from '../models';

export interface ApiWalletGetRequest {
    from?: Date;
    to?: Date;
}

/**
 * 
 */
export class WalletApi extends runtime.BaseAPI {

    /**
     */
    async apiWalletGetRaw(requestParameters: ApiWalletGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<WalletDto>>> {
        const queryParameters: any = {};

        if (requestParameters.from !== undefined) {
            queryParameters['from'] = (requestParameters.from as any).toISOString().substr(0,10);
        }

        if (requestParameters.to !== undefined) {
            queryParameters['to'] = (requestParameters.to as any).toISOString().substr(0,10);
        }

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/wallet`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(WalletDtoFromJSON));
    }

    /**
     */
    async apiWalletGet(requestParameters: ApiWalletGetRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<WalletDto>> {
        const response = await this.apiWalletGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiWalletTotalGetRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<FullWalletDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/wallet/total`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => FullWalletDtoFromJSON(jsonValue));
    }

    /**
     */
    async apiWalletTotalGet(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<FullWalletDto> {
        const response = await this.apiWalletTotalGetRaw(initOverrides);
        return await response.value();
    }

}
