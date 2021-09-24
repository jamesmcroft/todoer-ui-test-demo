import emitter from 'tiny-emitter/instance'

/**
 * Defines a default global emitter instance for listening and raising events across components.
 * 
 * This is added due to its removal from Vue 3 and the necessity to support migrating from Vue 2.
 */
export default {
  $on: (...args) => emitter.on(...args),
  $once: (...args) => emitter.once(...args),
  $off: (...args) => emitter.off(...args),
  $emit: (...args) => emitter.emit(...args)
}
