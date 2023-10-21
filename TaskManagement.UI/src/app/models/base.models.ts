export interface OdataContext<T> {
    "@odata.context": string;
    value: T;
}