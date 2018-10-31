## debian 9

出现 

* load library error: /lib/x86_64-linux-gnu/libm.so.6: version `GLIBC_2.27' not found (required by /home/yx/love/AppImage_run/liblove.so)

* 使用 `ldd --version` 查看  glibc 版本号 : ldd (Debian GLIBC 2.24-11+deb9u3) 2.24

* 解决方式1 ：升级 libc6
``` bash
sudo apt-get update
sudo apt-get install libc6
```
* 解决方式2 ：重新编译一个低版本的

dependence:

* libSDL2

``` bash
 sudo apt-get install libSDL2
```