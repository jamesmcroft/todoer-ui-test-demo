import { PropType } from "vue";
import { isObject, isUndefined } from "./inspect";

export const defineProp = <T = any>(type: PropType<T> = undefined, required: boolean = false, defaultValue: any = undefined) => {
  var def = isUndefined(defaultValue) ? {} : { default: isObject(defaultValue) ? () => defaultValue : defaultValue };

  return {
    ...(type ? { type } : {}),
    ...def,
    ...{ required }
  }
}

export const idProp = {
  id: defineProp(String, true)
}
