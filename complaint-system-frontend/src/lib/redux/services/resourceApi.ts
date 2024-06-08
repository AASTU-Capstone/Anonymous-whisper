import baseApi from "./baseApi";
import { CreateResourceInput } from "@/types";

const ResoureApi = baseApi.injectEndpoints({
    endpoints:(builder)=>({
        GetResourceById: builder.query<any,string>({
            query:(resourceId:string)=>`/Resource/GetResourceById?ResourceId=${resourceId}`
        }),

        GetAllResources: builder.query({
            query: () =>`/Resource/GetAllResources`
        }),

        CreateResource : builder.query<any, CreateResourceInput>({
            query:(createResource:CreateResourceInput) =>({
                url: `/Resource/CreateResource`,
                method:"POST",
                body:createResource
            })
        })
    }) 

        
});

export default ResoureApi;