export const isUndefined = (value: any) => value === undefined;

export const isNull = (value: any) => value === null;

export const isObject = (obj: any) => obj !== null && typeof obj === "object";

export const isUndefinedOrNull = (value: any) => isUndefined(value) || isNull(value);
